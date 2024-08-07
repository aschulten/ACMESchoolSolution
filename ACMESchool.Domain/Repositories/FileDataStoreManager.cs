using ACMESchool.Domain.Entities;
using ACMESchool.Domain.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ACMESchool.Domain.Repositories
{
    public class FileDataStoreManager : IStudentRepository, ICourseRepository
    {
        private readonly string filePath;

        public FileDataStoreManager(string filePath)
        {
            this.filePath = filePath;
        }

        // Método genérico para guardar cualquier entidad en el archivo.
        private void SaveEntity<T>(T entity) where T : BaseEntity
        {
            try
            {
                // Serializamos el objeto, añadiendo el tipo manualmente.
                string json = JsonConvert.SerializeObject(new
                {
                    Type = typeof(T).Name,
                    Entity = entity
                });

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(json);
                }
            }
            catch (IOException ex)
            {
                throw new DataStoreException($"Error while saving {typeof(T).Name}", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new DataStoreException($"Error while serializing {typeof(T).Name}", ex);
            }
        }

        // Método genérico para obtener una entidad por su ID.
        private T GetEntityById<T>(int id) where T : BaseEntity
        {
            try
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    var jsonObject = JObject.Parse(line);
                    string type = jsonObject["Type"]?.ToString();

                    // Verificamos si el tipo del objeto coincide con el tipo esperado.
                    if (type == typeof(T).Name)
                    {
                        T entity = jsonObject["Entity"].ToObject<T>();
                        if (entity.Id == id)
                        {
                            return entity;
                        }
                    }
                }
                throw new Exception($"Entity of type {typeof(T).Name} with ID {id} not found");
            }
            catch (IOException ex)
            {
                throw new DataStoreException($"Error while reading {typeof(T).Name}", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new DataStoreException($"Error while deserializing {typeof(T).Name}", ex);
            }
        }

        // Método genérico para eliminar una entidad por su ID.
        private void DeleteEntity<T>(int id) where T : BaseEntity
        {
            try
            {
                var newLines = File.ReadLines(filePath)
                                   .Where(line =>
                                   {
                                       var jsonObject = JObject.Parse(line);
                                       string type = jsonObject["Type"]?.ToString();

                                       if (type == typeof(T).Name)
                                       {
                                           T entity = jsonObject["Entity"].ToObject<T>();
                                           return entity == null || entity.Id != id;
                                       }
                                       return true;
                                   })
                                   .ToList();

                File.WriteAllLines(filePath, newLines);
            }
            catch (IOException ex)
            {
                throw new DataStoreException($"Error while deleting {typeof(T).Name}", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new DataStoreException($"Error while deserializing {typeof(T).Name}", ex);
            }
        }

        // Método genérico para actualizar una entidad.
        private void UpdateEntity<T>(T updatedEntity) where T : BaseEntity
        {
            try
            {
                var newLines = File.ReadLines(filePath)
                                   .Select(line =>
                                   {
                                       var jsonObject = JObject.Parse(line);
                                       string type = jsonObject["Type"]?.ToString();

                                       if (type == typeof(T).Name)
                                       {
                                           T entity = jsonObject["Entity"].ToObject<T>();
                                           return entity != null && entity.Id == updatedEntity.Id
                                               ? JsonConvert.SerializeObject(new { Type = type, Entity = updatedEntity })
                                               : line;
                                       }
                                       return line;
                                   })
                                   .ToList();

                File.WriteAllLines(filePath, newLines);
            }
            catch (IOException ex)
            {
                throw new DataStoreException($"Error while updating {typeof(T).Name}", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new DataStoreException($"Error while deserializing {typeof(T).Name}", ex);
            }
        }

        // Método genérico para obtener todas las entidades de un tipo.
        private List<T> GetAllEntities<T>() where T : BaseEntity
        {
            try
            {
                return File.ReadLines(filePath)
                           .Select(line =>
                           {
                               var jsonObject = JObject.Parse(line);
                               string type = jsonObject["Type"]?.ToString();

                               if (type == typeof(T).Name)
                               {
                                   return jsonObject["Entity"].ToObject<T>();
                               }
                               return null;
                           })
                           .Where(entity => entity != null)
                           .ToList();
            }
            catch (IOException ex)
            {
                throw new DataStoreException($"Error while reading {typeof(T).Name}s", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new DataStoreException($"Error while deserializing {typeof(T).Name}s", ex);
            }
        }


        public void SaveStudent(Student student) => SaveEntity(student);
        public Student GetStudentById(int id) => GetEntityById<Student>(id);
        public void DeleteStudent(int id) => DeleteEntity<Student>(id);
        public void UpdateStudent(Student student) => UpdateEntity(student);
        public List<Student> GetAllStudents() => GetAllEntities<Student>();


        public void SaveCourse(Course course) => SaveEntity(course);
        public Course GetCourseById(int id) => GetEntityById<Course>(id);
        public void DeleteCourse(int id) => DeleteEntity<Course>(id);
        public void UpdateCourse(Course course) => UpdateEntity(course);
        public List<Course> GetAllCourses() => GetAllEntities<Course>();

    }
}
