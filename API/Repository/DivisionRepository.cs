using API.Repository.Interface;
using API.ViewModels;
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
    public class DivisionRepository : IDivisionRepository
    {
        IConfiguration _configuration;

        DynamicParameters parameters = new DynamicParameters();
        public DivisionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int Create(DivisionVM divisionVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SPInsertDivision";
                parameters.Add("Name", divisionVM.Name);
                parameters.Add("DeptId", divisionVM.DeptId);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SPDeleteDivision";
                parameters.Add("Id", Id);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }

        public async Task<IEnumerable<DivisionVM>> Get()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SPGetAllDivision";
                var Get = await connection.QueryAsync<DivisionVM>(procName, parameters, commandType: CommandType.StoredProcedure);
                return Get;
            }
        }

        public DivisionVM Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SPGetByIdDivision";
                parameters.Add("Id", Id);
                var GetById = connection.Query<DivisionVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return GetById;
            }
        }

        //public async Task<IEnumerable<DivisionVM>> Get()
        //{
        //    using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
        //    {
        //        var procName = "SPGetAllDivision";
        //        var Get = await connection.QueryAsync<DivisionVM>(procName, commandType: CommandType.StoredProcedure);
        //        return Get;
        //    }
        //}

        //public async Task<IEnumerable<DivisionVM>> Get(int Id)
        //{
        //    using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
        //    {
        //        var procName = "SPGetByIdDivision";
        //        parameters.Add("Id", Id);
        //        var GetById = await connection.QueryAsync<DivisionVM>(procName, parameters, commandType: CommandType.StoredProcedure);
        //        return GetById;
        //    }
        //}

        public int Update(int Id, DivisionVM divisionVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SPUpdateDivision";
                parameters.Add("Id", Id);
                parameters.Add("Name", divisionVM.Name);
                parameters.Add("DeptId", divisionVM.DeptId);
                var Insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Insert;
            }
        }
    }
}
