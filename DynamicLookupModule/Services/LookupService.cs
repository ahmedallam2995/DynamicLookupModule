using DynamicLookupModule.Context;
using DynamicLookupModule.Enums;
using DynamicLookupModule.Helpers;
using DynamicLookupModule.Models;
using DynamicLookupModule.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicLookupModule.Services
{
    public class LookupService
    {
        DynamicLookupContext _context;

        public LookupService(DynamicLookupContext context)
        {
            _context = context;
        }


        public T GetLookup<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetLookupList<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }

        public bool AddLookup<T>(T lookup) where T : class
        {
            _context.Add(lookup);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateLookup<T>(T lookup) where T : class
        {
            _context.Update(lookup);
            return _context.SaveChanges() > 0;
        }

        public bool RemoveLookup<T>(int id) where T : class
        {
            _context.Remove(GetLookup<T>(id));
            return _context.SaveChanges() > 0;
        }


        #region HELPERS
        public string NameById(int Id)
        {
            var @enum = Enum.GetValues(typeof(LookupTypeEnum));
            foreach (var item in @enum)
            {
                if ((int)item == Id)
                {
                    return Constatnts.LookupEntityModelsNameSpace + item.ToString();
                }
            }
            return null;
        }

        public Type TypeById(int Id)
        {
            var @enum = Enum.GetValues(typeof(LookupTypeEnum));
            foreach (var item in @enum)
            {
                if ((int)item == Id)
                {
                    return Type.GetType(Constatnts.LookupEntityModelsNameSpace + item.ToString());
                }
            }
            return null;
        }

        public int IdByType(BaseLookup BL)
        {
            if (BL != null)
            {
                var @enum = Enum.GetValues(typeof(LookupTypeEnum));
                foreach (var item in @enum)
                {
                    if (BL.GetType() == Type.GetType(Constatnts.LookupEntityModelsNameSpace + item.ToString()))
                    {
                        return (int)item;
                    }
                }
            }
            return 0;
        }
        #endregion

    }
}
