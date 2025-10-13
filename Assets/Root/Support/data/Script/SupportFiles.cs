using UnityEngine;
using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameCore
{
    /// <summary>
    /// �T�|�[�g�f�[�^���̑�\�I�ȃt�@�C������ӏ��Œ�`���Ď擾�ł���w���p�[�B
    /// ALL_SOUND_BIN �ȂǁA�Ăяo�����͂��̖��O�����Q�Ƃ���΃t���p�X���Ԃ�B
    /// </summary>
    public static class SupportFiles
    {
        public const string SUPPORT_ROOT_NAME = "SupportChigadio";
        public const string SUPPORT_DATA_NAME = "data";

        // data�����̃t�H���_
        public const string ASSETS_FOLDER = "assets-data";
        public const string SOUND_FOLDER = "sound";
        public const string TEXTURE_FOLDER = "texture";
        public const string GAMEOBJECT_FOLDER = "gameobject";

        //dataID
        public const string ID_FOLDER = "class-data-id";
        public const string ID_BIN_FILE = "all_class_data.bin";

        //matrixID
        public const string MATRIX_DATA_ID_FOLDER = "class-data-matrix-id";
        public const string MATRIX_ID_BIN_FILE = "all_class_data_matrix.bin";

        // �t�@�C�����i����������`�����OK�j
        public const string ALL_SOUND_BIN_FILE = "sound_data.bin";
        public const string ALL_TEXTURE_BIN_FILE = "texture_data.bin";
        public const string ALL_GAMEOBJECT_BIN_FILE = "gameobject_data.bin";

        //Scenario
        public const string SCENARIO_FOLDER = "scenario-data";
        public const string SCENARIO_EVEMT_FOLDER = "scenario-event-data";
        public const string ALL_SCENARIO_EVENT_BIN_FILE = "all_events.bin";

        // �L���b�V���i�ŏ��ɉ��������p�X��ێ��j
        public static string s_cachedSupportDataPath = null;

        /// <summary>
        /// SupportChigadio/data �̃t���p�X���擾�i�L���b�V������^Editor�ł�AssetDatabase�����s�j
        /// </summary>
        private static string SupportDataPath
        {
            get
            {
                if (!string.IsNullOrEmpty(s_cachedSupportDataPath)) return s_cachedSupportDataPath;

#if UNITY_EDITOR
                // Editor�Ȃ�AssetDatabase�ł܂��T���i���������C���X���b�h�łȂ��Ɨ�O�ɂȂ�\��������̂� try/catch�j
                try
                {
                    string assetsRelative = FindFolderPathByAssetDatabase(SUPPORT_ROOT_NAME); // "Assets/..."
                    if (!string.IsNullOrEmpty(assetsRelative))
                    {
                        string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
                        string absoluteSupportRoot = Path.GetFullPath(Path.Combine(projectRoot, assetsRelative)); // -> .../Project/Assets/.../SupportChigadio
                        string dataPath = Path.Combine(absoluteSupportRoot, "..", SUPPORT_DATA_NAME);
                        s_cachedSupportDataPath = Path.GetFullPath(dataPath).Replace("\\", "/");
                        return s_cachedSupportDataPath;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"AssetDatabase lookup failed (maybe called from background thread): {e.Message}. Falling back to filesystem.");
                }
#endif
                // �t�@�C���V�X�e����ł̌��iprojectRoot/SupportChigadio/data�j
                string projectRootFs = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
                string candidate = Path.Combine(projectRootFs, SUPPORT_ROOT_NAME, SUPPORT_DATA_NAME);
                if (Directory.Exists(candidate))
                {
                    s_cachedSupportDataPath = Path.GetFullPath(candidate).Replace("\\", "/");
                    return s_cachedSupportDataPath;
                }

                // ����ł�������Ȃ���΃v���W�F�N�g���������i�d���\������j
                try
                {
                    var dirs = Directory.GetDirectories(projectRootFs, SUPPORT_ROOT_NAME, SearchOption.AllDirectories);
                    if (dirs != null && dirs.Length > 0)
                    {
                        string found = dirs[0];
                        string dataPath = Path.Combine(found, SUPPORT_DATA_NAME);
                        s_cachedSupportDataPath = Path.GetFullPath(dataPath).Replace("\\", "/");
                        return s_cachedSupportDataPath;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Fallback search failed: {ex.Message}");
                }

                // �Ō�̍ŏI��i�FProject������ GameData ���g��
                string fallback = Path.Combine(projectRootFs, "GameData");
                s_cachedSupportDataPath = Path.GetFullPath(fallback).Replace("\\", "/");
                return s_cachedSupportDataPath;
            }
        }

        /// <summary>
        /// ���ꂾ���Q�Ƃ���� all_sound.bin �̃t���p�X��������i�Ăяo�����͂��ꂾ������Ηǂ��j
        /// </summary>
        public static string ALL_SOUND_BIN => Path.GetFullPath(Path.Combine(SupportDataPath, ASSETS_FOLDER, SOUND_FOLDER, ALL_SOUND_BIN_FILE)).Replace("\\", "/");
        public static string ALL_TEXTURE_BIN => Path.GetFullPath(Path.Combine(SupportDataPath, ASSETS_FOLDER, TEXTURE_FOLDER, ALL_TEXTURE_BIN_FILE)).Replace("\\", "/");
        public static string ALL_GAMEOBJECT_BIN => Path.GetFullPath(Path.Combine(SupportDataPath, ASSETS_FOLDER, GAMEOBJECT_FOLDER, ALL_GAMEOBJECT_BIN_FILE)).Replace("\\", "/");
        public static string ALL_MATRIX_ID_BIN => Path.GetFullPath(Path.Combine(SupportDataPath, MATRIX_DATA_ID_FOLDER, MATRIX_ID_BIN_FILE)).Replace("\\", "/");
        public static string ALL_ID_BIN => Path.GetFullPath(Path.Combine(SupportDataPath, ID_FOLDER, ID_BIN_FILE)).Replace("\\", "/");
        public static string ALL_SCENARIO_EVENTS_BIN => Path.GetFullPath(Path.Combine(SupportDataPath, SCENARIO_FOLDER,SCENARIO_EVEMT_FOLDER, ALL_SCENARIO_EVENT_BIN_FILE)).Replace("\\", "/");

#if UNITY_EDITOR
        // Editor��p�FAssetDatabase�ŒT���� "Assets/..." ��Ԃ��i���s����� null�j
        private static string FindFolderPathByAssetDatabase(string folderName)
        {
            string[] guids = AssetDatabase.FindAssets("t:folder " + folderName, new[] { "Assets" });
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid); // "Assets/...."
                if (AssetDatabase.IsValidFolder(path) && Path.GetFileName(path) == folderName)
                    return path;
            }
            return null;
        }
#endif

        /// <summary>
        /// �⏕�F��΃p�X���v���W�F�N�g���iProject���[�g�j�Ɋ܂܂��Ȃ� "Assets/..." ���΃p�X��Ԃ��BAssetDatabase�nAPI�ɓn�������Ƃ��Ɏg����B
        /// </summary>
        public static string GetAssetRelativePath(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath)) return null;
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "..")).Replace("\\", "/");
            absolutePath = Path.GetFullPath(absolutePath).Replace("\\", "/");
            if (absolutePath.StartsWith(projectRoot, StringComparison.OrdinalIgnoreCase))
            {
                string rel = absolutePath.Substring(projectRoot.Length).TrimStart('/', '\\');
                return rel;
            }
            return null;
        }

        /// <summary>
        /// ���݊m�F�̃V���[�g�J�b�g
        /// </summary>
        public static bool ALL_SOUND_BIN_Exists => File.Exists(ALL_SOUND_BIN);
    }
}