using System;

namespace StoreApp.Library
{
    public interface IIdentifiable
    {
        Guid ID  { get; } 
    }
}