namespace Singleton
{
    public class LastTitleMenuSingleton
    {
        private static LastTitleMenuSingleton _instance;
        public int Index { get; set; }

        public static LastTitleMenuSingleton GetInstance()
        {
            _instance ??= new LastTitleMenuSingleton();
            return _instance;
        }
    }
}
