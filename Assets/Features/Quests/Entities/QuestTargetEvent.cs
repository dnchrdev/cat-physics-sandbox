using Features.Shared.Entities;

namespace Features.Quests.Entities
{
    public enum QuestTargetAction
    {
        Hit,
        Collided
    }

    public readonly struct QuestTargetEvent
    {
        public int TargetIndex { get; }
        public QuestTargetAction Action { get; }
        public float Height { get; }
        public float Speed { get; }
        public TeamType CollidedWith { get; }

        private QuestTargetEvent(int targetIndex, QuestTargetAction action, float height, float speed, TeamType collidedWith)
        {
            TargetIndex = targetIndex;
            Action = action;
            Height = height;
            Speed = speed;
            CollidedWith = collidedWith;
        }

        public static QuestTargetEvent Hit(int targetIndex) =>
            new(targetIndex, QuestTargetAction.Hit, 0f, 0f, TeamType.None);

        public static QuestTargetEvent Collided(int targetIndex, float height, float speed, TeamType collidedWith) =>
            new(targetIndex, QuestTargetAction.Collided, height, speed, collidedWith);
    }
}
