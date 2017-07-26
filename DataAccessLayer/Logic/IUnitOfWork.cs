using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Logic
{
   public interface IUnitOfWork
    {
         IRepositoryBase<User,int>  UserRepository { get; }
    }
}
