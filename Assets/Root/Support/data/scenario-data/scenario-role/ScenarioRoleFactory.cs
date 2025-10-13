using System;

namespace GameCore.Scenario {
    public static class ScenarioRoleFactory {
        public static BaseScenarioRoleData CreateRoleData(ScenarioRoleID id) {
            switch (id) {
                case ScenarioRoleID.TalkText:
                    return new TalkTextRoleData();
                default:
                    return null;
            }
        }

        public static BaseOrigintScenarioRoleAction CreateRoleAction(BaseScenarioRoleData data) {
            if (data == null) return null;
            switch (data.RoleID) {
                case ScenarioRoleID.TalkText:
                    return new TalkTextRoleAction(data as TalkTextRoleData);
                default:
                    return null;
            }
        }
    }
}
