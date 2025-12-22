#![allow(unused)]
mod cafeteria;
mod data_provider;
mod gift_shop;
mod lobby;
mod printing_department;
mod secret_entrance;

fn main() {
    //day_one();
    //day_two();
    //day_three();
    //day_four();
    day_five();
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

fn day_three() {
    println!("Running Day 3");
    let s = data_provider::load_data_from_file("day3.txt");
    let data = s.trim();
    lobby::run_part_one(&data);
    println!("\n");
    lobby::run_part_two(&data);
}

fn day_four() {
    println!("Running Day 4");
    let s = data_provider::load_data_from_file("day4.txt");
    let data = s.trim();
    printing_department::run_part_one(&data);
    println!("\n");
    printing_department::run_part_two(&data);
}

fn day_five() {
    println!("Running Day 5");
    let s = data_provider::load_data_from_file("day5.txt");
    let data = s.trim();
    cafeteria::run_part_one(&data);
}
