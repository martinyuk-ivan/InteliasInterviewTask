﻿using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Model;

namespace DataAccessLayer.Logic.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        Func<UserDbContext, IRepositoryBase<User, int>> userRepositoryInstantiator;
        IRepositoryBase<User, int> userRepository;
        UserDbContext context;
        public UnitOfWork(UserDbContext dbContext,Func<UserDbContext, IRepositoryBase<User,int>> userRepositoryInstantiator)
        {
            context = dbContext;
            this.userRepositoryInstantiator = userRepositoryInstantiator;
        }
        public IRepositoryBase<User, int> UserRepository
        {
            get
            {
                if (userRepository==null)
                {
                    userRepository = userRepositoryInstantiator(context);
                }
                return userRepository;
            } }
    }
}
