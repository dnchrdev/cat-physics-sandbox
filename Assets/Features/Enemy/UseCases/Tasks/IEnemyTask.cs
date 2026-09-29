namespace Features.Enemy.UseCases.Tasks
{
    public interface IEnemyTask
    {
        TaskResult Evaluate(float dt);
    }
}
