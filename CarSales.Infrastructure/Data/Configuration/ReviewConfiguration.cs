using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSales.Infrastructure.Data.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(CreateReviews());
        }

        private List<Review> CreateReviews()
        {
            var reviews = new List<Review>()
            {
                new Review()
                {
                    Id = 1,
                    VehicleId = 1,
                    ReviewerId = 1,
                    Title = "Rocket-ship propulsion, rear-drive mode adds spirit, luxe, comfy cabin.",
                    ReviewType = ReviewType.Standart,
                    ReviewStatus = ReviewStatus.Completed,
                    VehicleRating = VehicleRating.Good,
                    Overview = "Some cars are big-bodied and some are thrilling. The BMW M5 is both, with a body based on the regular 5-series and a heart-and-lung transplant courtesy of the brand's M performance division. Under the hood lives a spectacular 600-hp twin-turbo V-8 bolted to an eight-speed automatic transmission that powers all four wheels. An optional Competition package turns up the heat with 17 extra horsepower, a more soulful exhaust, stiffer suspension, and Competition badging and trim. That version rocketed to 60 mph in 2.8 seconds in our testing. Built to handle the rigors of mountain hairpins, blasts on the autobahn, and everyday life the M5 offers a premium experience with a penchant for fireworks. Unlike its closest competitor, the Cadillac CT5-V Blackwing, the Bimmer's stealthy packaging isn't offset by a thunderous exhaust but its impressive comfort and refinement make it among the best in the premium sports sedan segment.",
                    Performance = "The M5 is mighty, boasting 600 horsepower from its twin-turbo 4.4-liter V-8 in base form. In the more track-focused Competition, that figure increases to 617 horses. Believing that the Competition's V-8 had even more power than that we took that model to a dynamometer, were our suspicions proved true. We've also strapped our testing gear to both the regular M5 as well as the Competition model. Both met our expectations with brutally quick acceleration, sports-car-like cornering grip, and amazing stopping power. Likewise, the Competition proved to be quicker than the regular M5 on Virginia International Raceway's Grand Course at the 2019 Lightning Lap competition. Driving enjoyment is maximized here with lively and direct steering and a well-controlled albeit stiff ride. That doesn't mean the M5 can't also do duty as a luxury sedan: In Comfort mode, it cruises placidly, the cabin whisper quiet.",
                    Interior = "The M5 has an elegant leather interior with supportive sport seats. Unlike many of its rivals, BMW hasn't gone the all-touchscreen route, so adjusting the air conditioning or radio (via physical controls) is easy to do while the vehicle is in motion. The M5 comes with a slew of desirable features such as customizable ambient interior lighting, a heated steering wheel, heated front seats, and a power-adjustable steering column. BMW offers ventilated front seats with massage functionality, heated rear seats, and four-zone automatic climate control for more coin. As for storage space, the M5 has useful cubbies in the cabin, and its trunk held six carry-on suitcases in our testing.",
                    Price = 150,
                },
                new Review()
                {
                    Id = 2,
                    VehicleId = 2,
                    ReviewerId = 1,
                    ReviewType = ReviewType.Short,
                    ReviewStatus = ReviewStatus.Completed,
                    VehicleRating = VehicleRating.Reliable,
                    Title = "Three good powertrain options, high-end cabin materials, cutting-edge infotainment tech.",
                    Overview = "Driving a Bugatti Veyron is like carrying a 14.6-foot-long open wallet that is spewing 50-dollar bills. Drivers rush up from behind, tailgating before swerving into either of the Veyron’s rear-three-quarter blind spots, where they hang ape-like out of windows to snap photos with their cell phones. They won’t leave, either, because they know the Bugatti, averaging 11 mpg, can’t go far without refueling and that its driver will soon need to take a minute to compose himself. And when you open the Veyron’s door to exit—a gymnastic feat that requires grabbing one of your own ankles to drag it across that huge, hot sill—you will be greeted by 5 to 15 persons wielding cameras and asking questions. If you’re wearing shorts or a skirt, here’s a tip: Wear underwear.",
                    Performance = "The somewhat disappointing news is that despite accurate, nicely weighted steering and 1.00 g of skidpad grip, the car isn’t particularly nimble in the hills, where it is taxed by its 4486-pound heft. It feels more like a Benz SL63 AMG than, say, a BMW M3.\r\n\r\nThe Veyron’s weird shifter, which we named Klaatu, is as alien as the rest of the car. Push down for park. Push once to the right for drive. Twice to the right for sport mode. Left for neutral. Left and down for reverse. No matter where you shove it, it instantly returns to its original position, à la BMW turn signals. This is annoying, but resist the urge to abuse any gears. A new transmission costs $123,200. Speaking of abuse, within the 366-page hardcover owner’s manual, there are 190 boxed messages headlined “WARNING!”\r\n",
                    Price = 100,
                },
                new Review()
                {
                    Id = 3,
                    VehicleId = 3,
                    ReviewerId = 1,
                    ReviewType = ReviewType.Premium,
                    ReviewStatus = ReviewStatus.Completed,
                    VehicleRating = VehicleRating.Good,
                    Title = "Impeccable interior furnishings, high-tech features integrated throughout, two perky turbo engine choices.",
                    Overview = "With its subdued styling and straightforward-but-refined interior, the 2024 Audi A6 is a classic German luxury sedan. There's nothing garish or overtly flashy about its design, and its comfort-first demeanor means it's perfect for long-haul autobahn runs. Entry-level models are powered by a turbocharged four-cylinder, but we like the optional turbocharged V-6 which makes a potent 335 horsepower. The more powerful S6 model (reviewed separately) livens things up with 444 horsepower and a tauter suspension. Driving enthusiasts may want to go with the S6 for more on-road entertainment, but those seeking quiet luxury will find that in the A6. Rivals such as the BMW 5-series, the Genesis G80, and the Mercedes-Benz E-Classserve as natural comparisons to the Audi and all offer similar style, comfort, and quality.",
                    Performance = "The A6's two powertrains—a 261-hp turbocharged 2.0-liter four-cylinder and a 335-hp turbocharged 3.0-liter V-6—are both more than enough to haul this mid-size sedan around town without undue strain. Both powertrains employ hybrid technology with a 12- or 48-volt starter/alternator that runs the engine's stop-start system and other ancillary equipment. A seven-speed automatic transmission and Quattro all-wheel drive are both standard. The V-6 delivers plenty of thrust for merging and passing on the highway: at our test track, it charged from zero to 60 mph in just 4.8 seconds. Despite this quick result, it's not quite enough to outrun its key rivals, the BMW 540i xDrive and the Mercedes-Benz E450 4Matic. The 540i managed a 4.5-second run, while the Benz did it in 4.6. Thanks to its absorbent ride the A6 performs better as a luxury car than a sports sedan. We enjoyed its balanced handling and precise steering but never felt totally engaged when attacking twisty sections of road.",
                    Interior = "The A6's interior design is sleek, modern, and nicely put together from excellent-quality materials. Soft leather adorns the seats and armrests, rich-looking wood and nickel-finished metal trim is tastefully applied to the dash and doors, and the majority of the A6's secondary controls—climate, drive mode, etc.—are handled by a large touch-sensitive panel underneath the main infotainment display. A similar system is used in the A8 luxury sedan and the Q8 crossover, and despite our usual griping about the takeover of touchscreen controls, it works well and provides satisfying haptic feedback. A large trunk and easy-to-fold rear seatbacks make the A6 great for hauling cargo. We fit six of our carry-on suitcases in the trunk, which ties both the E450 and the 540i. The Audi offered far more space than either of those two with the rear seats folded, managing to hold 20 cases; the Benz held 18 and the BMW 16.",
                    Longevity = "Audi offers quite a few standard and optional driver-assistance features, including a system that watches out for traffic to save you from stepping out of the car and into the path of a moving vehicle. For more information about the A6's crash-test results, visit the National Highway Traffic Safety Administration (NHTSA) and Insurance Institute for Highway Safety (IIHS) websites.",
                    Features = "Audi offers an average standard warranty that when compared with other premium brands, looks pretty basic. Jaguar offers more value here, with longer warranties and five years of complimentary scheduled maintenance",
                    Price = 200,
                },
            };
            return reviews;
        }
    }
}
