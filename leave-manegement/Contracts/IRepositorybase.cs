﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace leave_manegement.Contracts
{
    public interface IRepositorybase<T> where T : class
    {
        ICollection<T> FindAll();
        T FindById(int id);
        bool isExists(int id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();
    }
}
