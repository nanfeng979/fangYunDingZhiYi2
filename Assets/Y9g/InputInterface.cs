namespace Y9g
{
    public interface ICommand
    {
        void Execute();
    }

    public interface IButtonClick : ICommand { }

    public interface IEscClick : ICommand { }

    public interface ISpace : ICommand { }

    public interface IMove
    {
        void OnMove(Move4Direction direction);
    }

    public interface IMoveDown 
    {
        void OnMoveDown(Move4Direction direction);
    }
}