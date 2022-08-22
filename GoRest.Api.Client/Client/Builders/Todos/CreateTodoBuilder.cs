using GoRest.Api.Client.Client.Models;

namespace GoRest.Api.Client.Client.Builder.TodosApi
{
    public class CreateTodoBuilder
    {
        private readonly CreateTodoModel _createTodo;

        public CreateTodoBuilder()
        {
            this._createTodo = GetDefault();
        }

        public CreateTodoBuilder With(Action<CreateTodoModel> updateAction)
        {
            updateAction.Invoke(this._createTodo);
            return this;
        }

        public CreateTodoModel Build()
        {
            return this._createTodo;
        }

        public CreateTodoModel GetDefault()
        {
            var model = new CreateTodoModel()
            {
                Title = new Random().Next(1111, 5555).ToString(),
                Due_On = DateTime.Now.AddYears(1),
                Status = "Pending",
            };

            return model;
        }
    }
}
