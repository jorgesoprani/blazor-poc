using Blazor.PoC.Presentation.Server.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.Server.Pages {
    public partial class PerformanceTestComponent {
        private BudgetPlanningModel model;
        [Inject] private NotificationsService _notifications { get; set; }

        public class Model {
            public int Years { get; set; }
            public int Rows { get; set; }
        }

        [Parameter]
        public Model Config { get; set; }

        protected override void OnInitialized() {
            Config = new() {
                Rows = 5,
                Years = 5
            };
        }

        protected async Task CreateModel() {
            try {
                model = new BudgetPlanningModel();
                model.Categories = new List<BudgetPlanningCategory>();
                var random = new Random();

                var categoriesCount = Config.Rows;
                var minYear = 2018;
                var yearsCount = Config.Years;


                for (int catIndex = 0; catIndex < categoriesCount; catIndex++) {
                    var group = (int)Math.Floor((double)catIndex / 5) + 1;

                    var category = new BudgetPlanningCategory();
                    category.Group = "Group " + (group + 1);
                    model.Categories.Add(category);
                    category.Category = $"Category {catIndex + 1}";
                    category.Years = new List<BudgetPlanningYear>();
                    for (int year = minYear; year < minYear + yearsCount; year++) {
                        var yearBudget = new BudgetPlanningYear();
                        yearBudget.Year = year;
                        yearBudget.Months = new List<BudgetPlanningMonth>();

                        for (int month = 1; month <= 12; month++) {
                            var value = ((year - minYear) + 1) * 100 + (catIndex * 100);
                            //var value = random.Next(100, 1000);
                            yearBudget.Months.Add(new BudgetPlanningMonth() {
                                Month = month,
                                Value = value
                            });
                        }

                        category.Years.Add(yearBudget);
                    }
                }

            } catch (Exception ex) {
                await _notifications.ShowError(ex.Message);
            }
        }


        public class BudgetPlanningModel {
            public int Id { get; set; }
            public int MinYear => Categories.Min(x => x.Years.Min(y => y.Year));
            public int MaxYear => Categories.Max(x => x.Years.Max(y => y.Year));
            public List<BudgetPlanningCategory> Categories { get; set; }
        }

        public class BudgetPlanningCategory {
            public string Category { get; set; }
            public string Group { get; set; }

            public List<BudgetPlanningYear> Years { get; set; }
        }

        public class BudgetPlanningYear {
            public int Year { get; set; }
            public List<BudgetPlanningMonth> Months { get; set; }
        }

        public class BudgetPlanningMonth {
            public int Month { get; set; }
            public decimal? Value { get; set; }
        }
    }
}
