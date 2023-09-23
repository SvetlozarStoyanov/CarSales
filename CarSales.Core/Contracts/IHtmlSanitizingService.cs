namespace CarSales.Core.Contracts
{
    public interface IHtmlSanitizingService
    {
        /// <summary>
        /// Removes all javascript code in object of type <typeparamref name="T"/> string properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        T SanitizeObject<T>(T obj);

        /// <summary>
        /// Removes javascript code from <paramref name="stringProperty"/> 
        /// </summary>
        /// <param name="stringProperty"></param>
        /// <returns></returns>
        string SanitizeStringProperty(string stringProperty);
    }
}
