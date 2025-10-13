using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GameCore.Scenario
{
    // ヘッダー全体を管理するクラス
    public class ScenarioEventBinaryHeader
    {
        // staticなフィールドでヘッダー情報を保持
        private static List<ScenarioEventInfo> _events = null;

        // 読み込んだイベントリストを返すプロパティ
        public static List<ScenarioEventInfo> Events
        {
            get
            {
                if (_events == null)
                {
                    _events = new List<ScenarioEventInfo>();
                }
                return _events;
            }
            private set
            {
                _events = value;
            }
        }

        public static async UniTask ReadHeaderAsync(Action action = null)
        {
            using (var stream = new FileStream(SupportFiles.ALL_SCENARIO_EVENTS_BIN, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream, Encoding.UTF8))
            {
                int eventCount = reader.ReadInt32();


                if (eventCount <= 0)
                {
                    throw new InvalidDataException($"Invalid event count: {eventCount}");
                }

                Events.Clear();
                for (int i = 0; i < eventCount; i++)
                {
                    int idLength = reader.ReadInt32();
                    if (idLength < 0 || idLength > 1000) // 妥当な長さチェック
                    {
                        throw new InvalidDataException($"Invalid event ID length: {idLength} at position {stream.Position - 4}");
                    }
                    string eventId = Encoding.UTF8.GetString(reader.ReadBytes(idLength));
                    int nameLength = reader.ReadInt32();
                    if (nameLength < 0 || nameLength > 1000)
                    {
                        throw new InvalidDataException($"Invalid event name length: {nameLength} at position {stream.Position - 4}");
                    }
                    string eventName = Encoding.UTF8.GetString(reader.ReadBytes(nameLength));
                    long eventOffset = reader.ReadInt64();
                    if (eventOffset < 0 || eventOffset > stream.Length)
                    {
                        throw new InvalidDataException($"Invalid event offset: {eventOffset} at position {stream.Position - 8}");
                    }

                    int subEventCount = reader.ReadInt32();
                    if (subEventCount < 0 || subEventCount > 1000)
                    {
                        throw new InvalidDataException($"Invalid subEvent count: {subEventCount} at position {stream.Position - 4}");
                    }

                    var subEvents = new List<ScenarioSubEventInfo>();
                    for (int j = 0; j < subEventCount; j++)
                    {
                        int subEventId = reader.ReadInt32();
                        int subNameLength = reader.ReadInt32();
                        if (subNameLength < 0 || subNameLength > 1000)
                        {
                            throw new InvalidDataException($"Invalid subEvent name length: {subNameLength} at position {stream.Position - 4}");
                        }
                        string subEventName = Encoding.UTF8.GetString(reader.ReadBytes(subNameLength));
                        long subEventOffset = reader.ReadInt64();
                        if (subEventOffset < 0 || subEventOffset > stream.Length)
                        {
                            throw new InvalidDataException($"Invalid subEvent offset: {subEventOffset} at position {stream.Position - 8}");
                        }
                        subEvents.Add(new ScenarioSubEventInfo(subEventId, subEventName, subEventOffset));
                    }

                    Events.Add(new ScenarioEventInfo(eventId, eventName, eventOffset, subEvents));
                    await UniTask.Yield();
                }
                await UniTask.Yield();
                action?.Invoke();
            }
        }

        // イベント名とサブイベントIDからサブイベントのシーク座標を取得するメソッド
        public static long GetSubEventOffset(string eventName, int subEventId)
        {
            if (_events == null)
            {
                throw new InvalidOperationException("Header has not been loaded. Call ReadHeaderAsync first.");
            }

            foreach (var eventInfo in _events)
            {
                if (eventInfo.EventName == eventName)
                {
                    foreach (var subEventInfo in eventInfo.SubEvents)
                    {
                        if (subEventInfo.SubEventId == subEventId)
                        {
                            return subEventInfo.SubEventOffset;
                        }
                    }
                }
            }

            throw new KeyNotFoundException($"SubEvent with ID {subEventId} in Event '{eventName}' not found.");
        }
    }
}