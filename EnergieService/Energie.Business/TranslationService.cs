//using Energie.Domain.Domain;
//using Energie.Infrastructure.ApplicationDbContext;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;


//namespace Energie.Business
//{
//    public class TranslationService<T> where T : class
//    {
//        private readonly AppDbContext _dbContext;
//        public TranslationService(AppDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<List<T>> GetTranslatedDataAsync<U>(string language) where U : class
//        {
//            try
//            {
//                var translations = await _dbContext.Translations.ToListAsync();
//                var items = await _dbContext.Set<T>().ToListAsync();
//                TranslateProperties(items, translations);

//                return items;
//            }
//            catch (Exception ex)
//            {
//                return new List<T>();
//            }
            
//        }

//        private void TranslateProperties(IEnumerable items, IEnumerable<Translations> translations)
//        {
//            try
//            {
//                foreach (var item in items)
//                {
//                    var properties = item.GetType().GetProperties()
//                        .Where(prop => prop.PropertyType == typeof(string) || prop.PropertyType.IsClass);

//                    try
//                    {
//                        foreach (var prop in properties)
//                    {
//                        if (prop.PropertyType == typeof(string))
//                        {
//                            TranslateStringProperty(item, prop, translations);
//                        }
//                        else
//                        {
//                            var value = prop.GetValue(item);
//                            if (value != null)
//                            {
//                                TranslateProperties(new[] { value }, translations);
//                            }
//                        }
//                    }
//                    }
//                    catch (Exception ex)
//                    {

//                    }
                    
//                }
//            }
//            catch (Exception ex)
//            {

//            }
            
//        }


//        private void TranslateStringProperty(object item, PropertyInfo prop, IEnumerable<Translations> translations)
//        {
//            var value = (string)prop.GetValue(item);
//            if (value != null)
//            {
//                var translation = translations.FirstOrDefault(t => t.NameKey == value);
//                if (translation != null)
//                {
//                    prop.SetValue(item, translation.Value);
//                }
//            }
//        }
//    }
//}
