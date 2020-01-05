namespace Tank_Dome.Model
{
    class TankFactory
    {
        public static Tank CreateTank(int tepy,int Left, int top)
        {
            Tank h;
            h = new Tank(tepy, Left, top);
            return h;
        }

    }
}
