using CompanyDirectory.Data;
using CompanyDirectory.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� ���������
builder.Services.AddControllersWithViews()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//============================================================
//���������� �������� ������
/*
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // ������� ����, ���� �� ���
    dbContext.Database.EnsureCreated();

    if (!dbContext.Departments.Any())
    {
        var rootDept = new Department
        {
            Name = "�������� ����",
            FormationDate = new DateTime(2020, 1, 1),
            Description = "�������� ������������� ��������"
        };

        dbContext.Departments.Add(rootDept);

        var itDept = new Department
        {
            Name = "IT �����",
            FormationDate = new DateTime(2020, 2, 1),
            Description = "����� �������������� ����������",
            ParentId = rootDept.Id
        };

        dbContext.Departments.Add(itDept);
        await dbContext.SaveChangesAsync();

        var employee1 = new Employee
        {
            FullName = "������ ���� ��������",
            BirthDate = new DateTime(1985, 5, 15),
            Gender = "�������",
            Position = "��������",
            HasDrivingLicense = true,
            DepartmentId = rootDept.Id
        };

        var employee2 = new Employee
        {
            FullName = "������� ���� ���������",
            BirthDate = new DateTime(1990, 8, 22),
            Gender = "�������",
            Position = "�����������",
            HasDrivingLicense = false,
            DepartmentId = itDept.Id
        };

        dbContext.Employees.Add(employee1);
        dbContext.Employees.Add(employee2);
        await dbContext.SaveChangesAsync();
    }
}
*/
//=====================================


app.Run();
