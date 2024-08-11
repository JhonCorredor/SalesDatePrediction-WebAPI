using Business.Interfaces;
using Data.Implements;
using Data.Interfaces;

namespace WebApi
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(IServiceCollection services)
        {
            // Employee
            services.AddScoped<IEmployeeBusiness, EmployeeBusiness>();
            services.AddScoped<IEmployeeData, EmployeeData>();

            // Product
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<IProductData, ProductData>();

            // Shipper
            services.AddScoped<IShipperBusiness, ShipperBusiness>();
            services.AddScoped<IShipperData, ShipperData>();

            // Supplier
            services.AddScoped<ISupplierBusiness, SupplierBusiness>();
            services.AddScoped<ISupplierData, SupplierData>();

            // Category
            services.AddScoped<ICategoryBusiness, CategoryBusiness>();
            services.AddScoped<ICategoryData, CategoryData>();

            // Customer
            services.AddScoped<ICustomerBusiness, CustomerBusiness>();
            services.AddScoped<ICustomerData, CustomerData>();

            // Order
            services.AddScoped<IOrderBusiness, OrderBusiness>();
            services.AddScoped<IOrderData, OrderData>();

            // OrderDetail
            services.AddScoped<IOrderDetailBusiness, OrderDetailBusiness>();
            services.AddScoped<IOrderDetailData, OrderDetailData>();

            // Lazy Employee
            services.AddScoped(provider => new Lazy<IEmployeeBusiness>(() => provider.GetRequiredService<IEmployeeBusiness>()));

            // Lazy Product
            services.AddScoped(provider => new Lazy<IProductBusiness>(() => provider.GetRequiredService<IProductBusiness>()));

            // Lazy Shipper
            services.AddScoped(provider => new Lazy<IShipperBusiness>(() => provider.GetRequiredService<IShipperBusiness>()));

            // Lazy Supplier
            services.AddScoped(provider => new Lazy<ISupplierBusiness>(() => provider.GetRequiredService<ISupplierBusiness>()));

            // Lazy Category
            services.AddScoped(provider => new Lazy<ICategoryBusiness>(() => provider.GetRequiredService<ICategoryBusiness>()));

            // Lazy Customer
            services.AddScoped(provider => new Lazy<ICustomerBusiness>(() => provider.GetRequiredService<ICustomerBusiness>()));

            // Lazy Order
            services.AddScoped(provider => new Lazy<IOrderBusiness>(() => provider.GetRequiredService<IOrderBusiness>()));

            // Lazy OrderDetail
            services.AddScoped(provider => new Lazy<IOrderDetailBusiness>(() => provider.GetRequiredService<IOrderDetailBusiness>()));
        }
    }

}
