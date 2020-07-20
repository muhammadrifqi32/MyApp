using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    public interface IDivisionRepository
    {
        Task<IEnumerable<DivisionVM>> Get();
        //IEnumerable<DivisionVM> Get();
        //Task<IEnumerable<DivisionVM>> Get(int Id);
        DivisionVM Get(int Id);
        int Create(DivisionVM divisionVM);
        int Update(int Id, DivisionVM divisionVM);
        int Delete(int Id);
    }
}
