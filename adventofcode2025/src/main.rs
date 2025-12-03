mod data_provider;
mod secret_entrance;
mod gift_shop;

fn main() {
    day_two();
}

fn day_one(){
    println!("Running Day 1");
    let data = data_provider::get_data_from_path("day1.txt");
    secret_entrance::run_part_one(&data);
    println!("\n");
    secret_entrance::run_part_two(&data);
}

fn day_two(){
    println!("Running Day 1");
    let data = data_provider::get_data_from_path("day2.txt");
    gift_shop::run_part_one(&data);
    println!("\n");
    gift_shop::run_part_two(&data);
}
