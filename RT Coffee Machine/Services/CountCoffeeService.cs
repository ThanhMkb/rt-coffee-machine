namespace RT_Coffee_Machine.Services
{
    public class CountCoffeeService : ICountCoffeeService
    {
        private int _count = 0;
        public CountCoffeeService()
        {

        }

        public void ResetCount()
        {
            _count = 0;
        }

        public int IncreaseCoffeCount()
        {
            _count++;
            return _count;
        }
    }
}
