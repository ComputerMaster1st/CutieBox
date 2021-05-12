namespace CutieBox.API.RandomApi
{
    public class RandomApi
    {
        public RandomApiImage Image { get; } = new RandomApiImage();
        public RandomApiFact Facts { get; } = new RandomApiFact();
    }
}
