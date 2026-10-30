using System;
using System.Configuration;
using Model;

namespace DataAccessLayer
{
    /// <summary>Выбор хранилища.</summary>
    public static class RepositoryFactory
    {
        /// <summary>Открыть хранилище.</summary>
        public static IRepository<Product> Create()
        {
            string mode = ConfigurationManager.AppSettings["Repository"] ?? "EF";
            if (mode != "EF" && mode != "Dapper")
                throw new ArgumentException("В App.config выберите EF или Dapper.");
            MarketplaceContext context = new MarketplaceContext();
            try
            {
                context.Database.Initialize(false);
                if (mode == "Dapper")
                {
                    context.Dispose();
                    return new DapperRepository<Product>(
                        ConfigurationManager.ConnectionStrings["MarketplaceDb"].ConnectionString);
                }
                return new EFRepository<Product>(context);
            }
            catch
            {
                context.Dispose();
                throw;
            }
        }
    }
}
