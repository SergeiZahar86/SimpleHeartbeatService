using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SimpleHeartbeatService
{
    class Program
    {
        static void Main(string[] args)
        {
            // x - вся конфигурация уровня хоста
            var exitCode = HostFactory.Run(x =>
            {
                // Service, SetServiceName, SetDisplayName, SetDescription, RunAsLocalSystem аргументы
                // команды извлекаются из переменных среды.

                // Service - конфигуратор хоста, говорим что есть такая служба как Heartbeat
                x.Service<Heartbeat>(s =>
                {
                    // параметры конфигурации службы через параметр 's'.
                    s.ConstructUsing(heartbeat => new Heartbeat()); // Это сообщает Topshelf, как создать экземпляр службы
                    s.WhenStarted(heartbeat => heartbeat.Start()); // указываем как Topshelf будет запускать службу 
                    s.WhenStopped(heartbeat => heartbeat.Stop()); // указываем как Topshelf будет останавливать службу
                });

                // указываем как его запустить. Здесь мы настраиваем «запуск от имени» и выбираем «локальную систему»
                x.RunAsLocalSystem();

                // Здесь мы настраиваем имя службы для использования WinService в мониторе службы Windows.
                x.SetServiceName("HeartbeatService");

                // Здесь мы настраиваем отображаемое имя для WinService, которое будет использоваться в мониторе службы Windows.
                x.SetDisplayName("Heartbeat Service");

                // Здесь мы настраиваем описание для WinService, которое будет использоваться в мониторе службы Windows.
                x.SetDescription("This is the sample heartbeat service used in a YouTube demo."); 
            });
            // Теперь, когда лямбда закрылась, конфигурация будет выполнена, и хост начнет работать


            // Наконец, мы конвертируем и возвращаем код выхода службы.
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
