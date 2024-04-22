﻿namespace PhotoSharingApi.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        List<T> GetAll();
    }
}
