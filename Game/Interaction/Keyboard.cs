namespace Game.Interaction
{
    public class Keyboard
    {
        public readonly Key HardDrop;
        public readonly Key Rotate;
        public readonly Key ShiftLeft;
        public readonly Key ShiftRight;
        public readonly Key SoftDrop;

        public Keyboard(GameController gameController, Scheduler scheduler)
        {
            Rotate = new Key();
            ShiftLeft = new RepeatingKey(scheduler,
                GameBoy.ShiftDelayFrames,
                GameBoy.ShiftIntervalFrames);
            ShiftRight = new RepeatingKey(scheduler,
                GameBoy.ShiftDelayFrames,
                GameBoy.ShiftIntervalFrames);
            SoftDrop = new RepeatingKey(scheduler,
                GameBoy.SoftDropIntervalFrames,
                GameBoy.SoftDropIntervalFrames);
            HardDrop = new Key();

            Rotate.KeyPress += gameController.HandleRotate;
            ShiftLeft.KeyPress += gameController.HandleShiftLeft;
            ShiftRight.KeyPress += gameController.HandleShiftRight;
            SoftDrop.KeyPress += gameController.HandleSoftDrop;
            HardDrop.KeyPress += gameController.HandleHardDrop;
        }
    }
}
