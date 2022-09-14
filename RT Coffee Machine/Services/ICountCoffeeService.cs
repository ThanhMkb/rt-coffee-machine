namespace RT_Coffee_Machine.Services
{
    public interface ICountCoffeeService
    {
        public void ResetCount();
        public int IncreaseCoffeCount();
    }
}
