#![allow(unused)]
mod data_provider;
mod gift_shop;
mod secret_entrance;
mod lobby;

fn main() {
    //day_one();
    //day_two();
    day_three();
}

fn day_one() {
    println!("Running Day 1");
    let s = data_provider::load_data_from_file("day1.txt");
    let data = s.trim();
    secret_entrance::run_part_one(&data);
    println!("\n");
    secret_entrance::run_part_two(&data);
}

fn day_two() {
    println!("Running Day 2");
    let s = data_provider::load_data_from_file("day2.txt");
    let data = s.trim();
    gift_shop::run_part_one(&data);
    println!("\n");
    gift_shop::run_part_two(&data);
}

fn day_three(){
    println!("Running Day 3");
    let s = data_provider::load_data_from_file("day2.txt");
    let data = s.trim();
    lobby::run_part_one(&data);
    println!("\n");
    lobby::run_part_two(&data);
}
