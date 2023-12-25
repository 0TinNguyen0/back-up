//using SV20T1080053.DomainModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Dapper;
//using System.Data;
//using Microsoft.Data.SqlClient;

//namespace SV20T1080053.DataLayers.SQLServer
//{
//    public class ProductDAL : _BaseDAL, ICommonDAL<Product>
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="connectionString"></param>
//        public ProductDAL(string connectionString) : base(connectionString)
//        {
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public int Add(Product data)
//        {
//            int id = 0;
//            using (var connection = OpenConnection())
//            {
//                var sql = @"if exists(select * from Products where Email = @Email)
//                                select -1
//                            else
//                                begin
//                                    insert into Products(CustomerName,ContactName,Province,Address,Phone,Email,IsLocked)
//                                    values(@CustomerName,@ContactName,@Province,@Address,@Phone,@Email,@IsLocked);
//                                    select @@identity;
//                                end";
//                var parameters = new
//                {
//                    customerName = data.CustomerName ?? "",
//                    contactName = data.ContactName ?? "",
//                    Province = data.Province ?? "",
//                    Address = data.Address ?? "",
//                    Phone = data.Phone ?? "",
//                    Email = data.Email ?? "",
//                    IsLocked = data.IsLocked
//                };
//                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
//                connection.Close();
//            }
//            return id;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="searchValue"></param>
//        /// <returns></returns>
//        public int Count(string searchValue = "")
//        {
//            int count = 0;
//            if (!string.IsNullOrEmpty(searchValue))
//                searchValue = "%" + searchValue + "%";
//            using (var connection = OpenConnection())
//            {
//                var sql = @"select count(*) from Products
//                            where (@searchValue = N'') or (ProductName like @searchValue)";
//                var parameters = new { searchValue = searchValue };
//                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
//                connection.Close();
//            }

//            return count;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public bool Delete(int id)
//        {
//            bool result = false;
//            using (var connection = OpenConnection())
//            {
//                var sql = "delete from Products where ProductId = @productId and not exists(select * from Orders where ProductId = @productId)";
//                var parameters = new { productId = id };
//                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
//                connection.Close();
//            }
//            return result;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public Product? Get(int id)
//        {
//            Product? data = null;
//            using (var connection = OpenConnection())
//            {
//                var sql = "select * from Products where ProductId = @productId";
//                var parameters = new { productId = id };
//                data = connection.QueryFirstOrDefault<Product>(sql: sql, param: parameters, commandType: CommandType.Text);
//                connection.Close();
//            }
//            return data;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public bool InUsed(int id)
//        {
//            bool result = false;
//            using (var connection = OpenConnection())
//            {
//                var sql = @"if exists(select * from Orders where ProductId = @productId)
//                                select 1
//                            else 
//                                select 0";
//                var parameters = new { productId = id };
//                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: CommandType.Text);
//                connection.Close();
//            }
//            return result;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="page"></param>
//        /// <param name="pageSize"></param>
//        /// <param name="searchValue"></param>
//        /// <returns></returns>
//        public IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "")
//        {
//            List<Product> data;
//            if (!string.IsNullOrEmpty(searchValue))
//                searchValue = "%" + searchValue + "%";
//            using (var connection = OpenConnection())
//            {
//                var sql = @"with cte as
//                (
//	                select *, row_number() over (order by ProductName) as RowNumber
//	                from CProducts
//	                where (@searchValue = N'') or (ProductName like @searchValue)
//                )
//	            select * from cte
//	            where (@pageSize=0)
//		            or(RowNumber between (@page -1) * @pageSize + 1 and @page * @pageSize)
//	            order by RowNumber";
//                var parameters = new
//                {
//                    page,
//                    pageSize,
//                    searchValue
//                };
//                data = connection.Query<Product>(sql: sql, param: parameters, commandType: CommandType.Text).ToList();
//                connection.Close();
//            }
//            if (data == null)
//                data = new List<Product>();
//            return data;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public bool Update(Product data)
//        {
//            bool result = false;
//            using (var connection = OpenConnection())
//            {
//                var sql = @"if not exists(select * from Products where ProductId <> @productId and Email = @email)
//                                begin
//                                    update Products 
//                                    set ProductName = @productName,
//                                        ContactName = @contactName,
//                                        Province = @province,
//                                        Address = @address,
//                                        Phone = @phone,
//                                        Email = @email,
//                                        IsLocked = @isLocked
//                                    where ProductId = @productId
//                                end";
//                var parameters = new
//                {
//                    customerId = data.CustomerID,
//                    customerName = data.CustomerName ?? "",
//                    contactName = data.ContactName ?? "",
//                    Province = data.Province ?? "",
//                    Address = data.Address ?? "",
//                    Phone = data.Phone ?? "",
//                    Email = data.Email ?? "",
//                    IsLocked = data.IsLocked
//                };
//                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
//            }
//            return result;
//        }
//    }
//}
