using RecipeClient.Database.Core;
using RecipeClient.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeClient.Database.InMemory
{
    public class InMemMeasureUnit : IRepository<MeasureUnit>
    {
        private List<MeasureUnit> MeasureUnits { get; } = new List<MeasureUnit>();

        public void Create(MeasureUnit entity)
        {
            MeasureUnits.Add(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MeasureUnit> Get(Func<MeasureUnit, bool> filter)
        {
            return MeasureUnits.Where(filter);
        }

        public MeasureUnit GetSingle(Func<MeasureUnit, bool> filter)
        {
            MeasureUnit? unit = MeasureUnits.FirstOrDefault(filter);

            if (unit is null)
            {
                throw new Exception(
                    "Unit does not exists");
            }

            return unit;
        }

        public void Update(MeasureUnit entity)
        {
            MeasureUnit unit = GetSingle(x => x.Id == entity.Id);

            int index = MeasureUnits.IndexOf(unit);

            MeasureUnits[index]= entity;    
        }
    }
}
