﻿namespace ResourceManagment.Data.Database.Schema
{
    public interface ISqlColumn
    {
        string BuildCreateQuery();
        string Name { get; }
    }
}