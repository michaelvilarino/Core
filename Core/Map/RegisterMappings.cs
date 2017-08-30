using Dapper.FluentMap;
using Dapper.FluentMap.Configuration;
using Dapper.FluentMap.Dommel;

namespace UTestProject.Map
{
    public static class RegisterMappings
    {
        static FluentMapConfiguration Config = new FluentMapConfiguration();

        public static void Register<T>() where T: class
        {                       
          GenericObjectMap<T> ObjectMap = new GenericObjectMap<T>();
          Config.AddMap(ObjectMap);                                   
        }

        public static void InitializeConfigMappings()
        {
            FluentMapper.Initialize(config => { Config.ForDommel(); });
        }
    }

}
