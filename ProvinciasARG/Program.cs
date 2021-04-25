using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using ProvinciasARG.Models; // Determina que para este scrip quiero utilizar este modelo.
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;

namespace ProvinciasARG
{
    class Program
    {
        static async Task Main(string[] args)
        {           
            var contenido = File.ReadAllText("provincias.json");
            var usuariopass = File.ReadAllText("usuariopass.json");
            
            
            string url = "https://apis.datos.gob.ar/georef/api/provincias";
            HttpClient client = new HttpClient();

            var httpResponse = await client.GetAsync(url);
            

            Console.WriteLine("Ingrese un Usuario.");
            var usuario = Console.ReadLine();
            Console.WriteLine("Ingrese su Password.");
            var pass = Console.ReadLine();
            
            List<Models.Usuariopass> posts2 =
                    JsonSerializer.Deserialize<List<Models.Usuariopass>>(usuariopass);

            var compusuariopass = from d in posts2
                                  where d.usuario == usuario
                                  //orderby d.nombre
                                  select d;

            var estadousuario = false;
            foreach (var g in compusuariopass)
                {
                 
                //Console.WriteLine($"Usuario: {g.usuario} {g.pass}");
                estadousuario = true;
                    if (g.pass == pass)
                    {
                    Console.WriteLine("Usuario y contraseña correctas.");
                    Console.WriteLine("****************************************************************");
                    Console.WriteLine("Ingrese el nombre de la provincia que quiere tener la ubicacion.");
                    var nombreprov = Console.ReadLine();

                    /*
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var content = await httpResponse.Content.ReadAsStringAsync();
                    */
                        List<Models.Post> posts =
                            JsonSerializer.Deserialize<List<Models.Post>>(contenido);

                        var provinciasordenadas = from d in posts
                                                  where d.nombre == nombreprov
                                                  //orderby d.nombre
                                                  select d;
                    var estadoprovincia = false;
                    foreach (var prov in provinciasordenadas)
                        {
                        estadoprovincia = true;
                            Console.WriteLine($"Provincia: {prov.nombre}");
                            Console.WriteLine($"Latitud: { prov.centroide.lat}");
                            Console.WriteLine($"Longitud: { prov.centroide.lon}");
                            var pp = JsonSerializer.Serialize(prov);
                            File.WriteAllText("provinciaconsultada.json", pp);
                        }
                    
                    if (estadoprovincia == false)
                    {
                        Console.WriteLine("Provincia no registrada.");
                    }
                    //Console.WriteLine(provinciasordenadas);
                    //Console.WriteLine(contenido);
                    /*    
                    }
                    */
                } else
                    {
                        Console.WriteLine("Contraseña incorecta.");
                    }
                }

            if (estadousuario == false)
            {
                Console.WriteLine("Usuario no registrado.");
            }
                

        }

    }
}
