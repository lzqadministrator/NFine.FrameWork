﻿using log4net;
using System;
using System.IO;
using System.Web;

namespace NFine.Code.Log
{
    public class LogFactory
    {
        static LogFactory()
        {
            FileInfo configInfo = new FileInfo(HttpContext.Current.Server.MapPath("/Configs/log4net.config"));
            log4net.Config.XmlConfigurator.Configure(configInfo);
        }

        public static Log GetLogger(Type type)
        {
            return new Log(LogManager.GetLogger(type));
        }

        public static Log GetLogger(string str)
        {
            return new Log(LogManager.GetLogger(str));
        }
    }
}
