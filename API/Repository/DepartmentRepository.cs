using API.Models;
using API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        IConfiguration _configuration;

        DynamicParameters parameters = new DynamicParameters();
        public DepartmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int Create(Department department)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SPInsertDepartment";
                parameters.Add("Name", department.Name);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SPDeleteDepartment";
                parameters.Add("Id", Id);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }

        public async Task<IEnumerable<Department>> Get()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SPGetAllDepartment";
                var Get = await connection.QueryAsync<Department>(procName, parameters, commandType: CommandType.StoredProcedure);
                return Get;
            }
        }

        public Department Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SPGetByIdDepartment";
                parameters.Add("Id", Id);
                var GetById = connection.Query<Department>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return GetById;
            }
        }

        //Fix
        //public async Task<IEnumerable<Department>> Get(int Id)
        //{
        //    using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
        //    {
        //        var procName = "SPGetByIdDepartment";
        //        parameters.Add("Id", Id);
        //        var GetById = await connection.QueryAsync<Department>(procName, parameters, commandType: CommandType.StoredProcedure);
        //        return GetById;
        //    }
        //}
        //fix

        public int Update(int Id, Department department)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SPUpdateDepartment";
                parameters.Add("Id", Id);
                parameters.Add("Name", department.Name);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }

        //fix
        //public async Task<IEnumerable<Department>> Get()
        //{
        //    using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
        //    {
        //        var procName = "SPGetAllDepartment";
        //        var Get = await connection.QueryAsync<Department>(procName, commandType: CommandType.StoredProcedure);
        //        return Get;
        //    }
        //}
        //fix
    }
}
