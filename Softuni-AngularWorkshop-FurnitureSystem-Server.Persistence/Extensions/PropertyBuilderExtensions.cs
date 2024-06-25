﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.Extensions
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new()
        {
            var converter = new ValueConverter<T, string>
            (
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<T>(v) ?? new T()
            );

            var comparer = new ValueComparer<T>
            (
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
                v => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(v))
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            //propertyBuilder.HasColumnType("jsonb");

            return propertyBuilder;
        }

        public static PropertyBuilder<decimal> HasPrecision(this PropertyBuilder<decimal> propertyBuilder, ushort precision, ushort scale)
        {
            propertyBuilder.HasColumnType($"decimal({precision}, {scale})");
            return propertyBuilder;
        }

        public static PropertyBuilder<decimal?> HasPrecision(this PropertyBuilder<decimal?> propertyBuilder, ushort precision, ushort scale)
        {
            propertyBuilder.HasColumnType($"decimal({precision}, {scale})");
            return propertyBuilder;
        }
    }
}