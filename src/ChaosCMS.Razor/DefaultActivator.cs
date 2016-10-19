﻿using System;
using System.Reflection;

namespace ChaosCMS.Razor
{
	public class DefaultActivator : IActivator
	{
        IServiceProvider services;
        public DefaultActivator(IServiceProvider services)
        {
            this.services = services;
        }

		public object CreateInstance(Type type)
		{
			var obj = Activator.CreateInstance(type);
            foreach(var prop in obj.GetType().GetProperties())
            {
                var value = services.GetService(prop.PropertyType);
                prop.SetValue(obj, value);
            }
            
            return obj;
		}
	}
}