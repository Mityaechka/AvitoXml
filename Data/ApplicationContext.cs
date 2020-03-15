
using AvitoXml.Wpf.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvitoXml.Wpf.Data
{
    class ApplicationContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
            if (Categories.Count() == 0)
            {
                Categories.AddRange(new List<Category>(){
                    new Category()
                    {
                        Name = "Бытовая техника",
                        Types = new List<Models.Type>()
                        {
                            new Models.Type(){
                                Name = "Бритвы и триммеры"
                            },
                            new Models.Type(){
                                Name = "Вентиляторы"
                            },
                            new Models.Type(){
                                Name = "Вытяжки"
                            },
                            new Models.Type(){
                                Name = "Другое"
                            },
                            new Models.Type(){
                                Name = "Кондиционеры"
                            },
                            new Models.Type(){
                                Name = "Машинки для стрижки"
                            },
                            new Models.Type(){
                                Name = "Мелкая кухонная техника"
                            },
                            new Models.Type(){
                                Name = "Микроволновые печи"
                            },
                            new Models.Type(){
                                Name = "Обогреватели"
                            },
                            new Models.Type(){
                                Name = "Очистители воздуха"
                            },
                            new Models.Type(){
                                Name = "Плиты"
                            },
                            new Models.Type(){
                                Name = "Посудомоечные машины"
                            },
                            new Models.Type(){
                                Name = "Пылесосы"
                            },
                            new Models.Type(){
                                Name = "Стиральные машины"
                            },
                            new Models.Type(){
                                Name = "Термометры и метеостанции"
                            },
                            new Models.Type(){
                                Name = "Утюги"
                            },
                            new Models.Type(){
                                Name = "Фены и приборы для укладки"
                            },
                            new Models.Type(){
                                Name = "Холодильники и морозильные камеры"
                            },
                            new Models.Type(){
                                Name = "Швейные машины"
                            },
                            new Models.Type(){
                                Name = "Эпиляторы"
                            }
                        },
                    },
                    new Category()
                    {
                        Name = "Мебель и интерьер",
                        Types = new List<Models.Type>()
                        {
                            new Models.Type(){
                                Name = "Другое"
                            },
                            new Models.Type(){
                                Name = "Компьютерные столы и кресла"
                            },
                            new Models.Type(){
                                Name = "Кровати, диваны и кресла"
                            },
                            new Models.Type(){
                                Name = "Кухонные гарнитуры"
                            },
                            new Models.Type(){
                                Name = "Предметы интерьера, искусство"
                            },
                            new Models.Type(){
                                Name = "Столы и стулья"
                            },
                            new Models.Type(){
                                Name = "Текстиль и ковры"
                            },
                            new Models.Type(){
                                Name = "Шкафы и комоды"
                            }
                        },
                    },
                    new Category()
                    {
                        Name = "Посуда и товары для кухни",
                        Types = new List<Models.Type>()
                        {
                            new Models.Type(){
                                Name = "Посуда"
                            },
                            new Models.Type(){
                                Name = "Товары для кухни"
                            }
                        },
                    },
                });
                SaveChanges();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "avito.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}
