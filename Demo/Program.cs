using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
    var app = builder.Build();
    string message = "";
    app.UseCors();
List<Order> repo =
[
   new(1, new DateOnly(2023,06,06), "Компьютер", "DEXP Aquilon O286", "Перестал работать", "Дмитрий Хорьков Ильич", "79024456469", "", "ожидание запчастей"),
   

];
    app.MapGet("/orders", () => repo);
    app.MapGet("/create", ([AsParameters] Order o) => repo.Add(o));
    app.MapGet("/update", ([AsParameters] UpdateDTO dto) =>
{
    var order = repo.Find(x => x.Number == dto.Number);
    if (order == null)
        return;
    if (dto.Status != order.Status && dto.Status != "")
    {
        order.Status = dto.Status;
        message += $"Заявка {order.Number} завершена";
    }
    if (dto.Master != "")
        order.Master = dto.Master;
});
    app.Run();

record class UpdateDTO(int Number, string? Status = "", string? Master = "", string? Comment = "");
class Order
{
    int number;
    DateOnly startDate;
    string equipment;
    string model;
    string problem;
    string fio;
    string phone;
    string? master = "";
    string status;

    public Order(int number, DateOnly startDate, string equipment, string model, string problem, string fio, string phone, string master, string status)
    {
        this.Number = number;
        this.StartDate = startDate;
        this.Equipment = equipment;
        this.Model = model;
        this.Problem = problem;
        this.Fio = fio;
        this.Phone = phone;
        this.Master = master;
        this.Status = status;
    }
    public int Number { get => number; set => number = value; }
    public DateOnly StartDate { get => startDate; set => startDate = value; }
    public string Equipment { get => equipment; set => equipment = value; }
    public string Model { get => model; set => model = value; }
    public string Problem { get => problem; set => problem = value; }
    public string Fio { get => fio; set => fio = value; }
    public string Phone { get => phone; set => phone = value; }
    public string? Master { get => master; set => master = value; }
    public string Status { get => status; set => status = value; }
}