using System;
using System.IO;
using UnityEngine;

namespace XrShared.Core.Data
{
    public sealed class JsonPersistenceStore : IPersistenceStore
    {
        public int SchemaVersion { get; }
        private readonly string _fileName;

        private string PathOnDisk => System.IO.Path.Combine(Application.persistentDataPath, _fileName);

        public JsonPersistenceStore(string fileName, int schemaVersion)
        {
            _fileName = string.IsNullOrWhiteSpace(fileName) ? "save.json" : fileName;
            SchemaVersion = schemaVersion <= 0 ? 1 : schemaVersion;
        }

        public void Save<T>(T data) where T : class
        {
            if (data == null) return;

            SaveSchema<T> schema = new SaveSchema<T>
            {
                schemaVersion = SchemaVersion,
                savedAtIsoUtc = DateTime.UtcNow.ToString("O"),
                payload = data
            };

            string json = JsonUtility.ToJson(schema, true);
            File.WriteAllText(PathOnDisk, json);
        }

        public bool TryLoad<T>(out T data) where T : class
        {
            data = null;

            if (!File.Exists(PathOnDisk)) return false;

            try
            {
                string json = File.ReadAllText(PathOnDisk);
                SaveSchema<T> schema = JsonUtility.FromJson<SaveSchema<T>>(json);

                if (schema == null || schema.payload == null) return false;
                data = schema.payload;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete()
        {
            if (File.Exists(PathOnDisk))
                File.Delete(PathOnDisk);
        }
    }
}
