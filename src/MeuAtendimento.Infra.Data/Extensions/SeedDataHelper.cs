using MeuAtendimento.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MeuAtendimento.Infra.Data.Extensions
{
    public static class SeedDataHelper
    {
        #region Metodos Publicos

        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            #region Especialidade

            builder.Entity<Especialidade>()
                   .HasData(new Especialidade
                   {
                       ID = new Guid("EACD332C-095D-4E7E-A48C-F5F58DEF0454"),
                       Nome = "Alergia e imunologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("A22D9E00-C329-4EF0-AD9E-57EDBF500D02"),
                       Nome = "Angiologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("08CFC5C5-C308-4DA2-AD9D-729ECC4B25B2"),
                       Nome = "Cardiologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("D7C4FB7B-511F-4240-A8B5-479A90243A9B"),
                       Nome = "Clínica Geral"
                   },
                   new Especialidade
                   {
                       ID = new Guid("23E86853-5BC2-422E-BF13-1B591CB15DEA"),
                       Nome = "Dermatologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("3c7c42c1-469f-4e98-9e49-ebb121157555"),
                       Nome = "Endoscopia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("f2c6cc58-4b34-4822-a5a1-b9d001dd411d"),
                       Nome = "Gastroenterologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("80b1e728-0a9a-4243-8169-68ec7a695181"),
                       Nome = "Geriatria"
                   },
                   new Especialidade
                   {
                       ID = new Guid("5b7128f9-be52-4f13-a10f-83a078fa6487"),
                       Nome = "Ginecologia e obstetrícia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("57414040-b5ab-4ff8-bdf8-085bd6c01920"),
                       Nome = "Hematologia e hemoterapia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("890a75d3-e68e-473e-a8d9-dcdde6a03dd9"),

                       Nome = "Homeopatia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("22f549e4-19ca-47f0-b076-219f88d56414"),
                       Nome = "Infectologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("ec8028ab-460d-4952-b84a-0792bd9c1c9b"),
                       Nome = "Mastologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("b8be6bd7-27fd-4dfa-a515-df3be69dc076"),
                       Nome = "Neurologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("135e1bfd-8846-4c5b-999c-f76d58e3e3a6"),
                       Nome = "Nutrologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("c651f453-f817-4e4c-856e-69e38fea2b20"),
                       Nome = "Oftalmologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("c2a13d28-e5fe-405b-b79d-7427fb2bff85"),
                       Nome = "Oncologia clínica"
                   },
                   new Especialidade
                   {
                       ID = new Guid("1f653a87-e6f0-4958-84b3-c55b1084ff00"),
                       Nome = "Ortopedia e traumatologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("19a9647e-7e93-42c6-8822-2ee18d37cdbf"),
                       Nome = "Otorrinolaringologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("2146df9f-fb87-431d-add7-fcc4a0175bc0"),
                       Nome = "Pediatria"
                   },
                   new Especialidade
                   {
                       ID = new Guid("fa0eaea2-f4f9-4f5f-9ec1-16aa8c4752d8"),
                       Nome = "Pneumologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("93573e95-64bf-4eb9-be71-478ed4f461a8"),
                       Nome = "Psiquiatria"
                   },
                   new Especialidade
                   {
                       ID = new Guid("ae74d1d6-8e64-4460-9765-adb7c37dc6e2"),
                       Nome = "Radiologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("9dc8a128-5d3a-4f9f-8a5e-3eb9b5b4bb8a"),
                       Nome = "Radioterapia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("12bdaf04-3d97-4d5f-b9db-42f588025510"),
                       Nome = "Reumatologia"
                   },
                   new Especialidade
                   {
                       ID = new Guid("bb510cea-86c4-4766-a448-e2f91a0e08e3"),
                       Nome = "Urologia"
                   });

            #endregion Especialidade

            return builder;
        }

        #endregion Metodos Publicos
    }
}