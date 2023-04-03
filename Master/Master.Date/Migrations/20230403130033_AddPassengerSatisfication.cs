using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPassengerSatisfication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PassengerSatisfication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    TypeOfTravel = table.Column<byte>(type: "tinyint", nullable: false),
                    Class = table.Column<byte>(type: "tinyint", nullable: false),
                    FlightDistance = table.Column<int>(type: "int", nullable: false),
                    WifiService = table.Column<int>(type: "int", nullable: false),
                    ArrivalTime = table.Column<int>(type: "int", nullable: false),
                    EaseOfBooking = table.Column<int>(type: "int", nullable: false),
                    GateLocation = table.Column<int>(type: "int", nullable: false),
                    FoodAndDrink = table.Column<int>(type: "int", nullable: false),
                    OnlineBoarding = table.Column<int>(type: "int", nullable: false),
                    SeatComfort = table.Column<int>(type: "int", nullable: false),
                    InflightEntertainment = table.Column<int>(type: "int", nullable: false),
                    OnBoardService = table.Column<int>(type: "int", nullable: false),
                    LegRoomService = table.Column<int>(type: "int", nullable: false),
                    BaggageHandling = table.Column<int>(type: "int", nullable: false),
                    CheckinService = table.Column<int>(type: "int", nullable: false),
                    InflightService = table.Column<int>(type: "int", nullable: false),
                    Cleanliness = table.Column<int>(type: "int", nullable: false),
                    DepartureDelayInMinutes = table.Column<int>(type: "int", nullable: false),
                    ArrivalDelayInMinutes = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerSatisfication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassengerSatisfication_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassengerSatisfication_UserId",
                table: "PassengerSatisfication",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassengerSatisfication");
        }
    }
}
