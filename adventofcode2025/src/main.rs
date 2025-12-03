mod data_provider;
mod secret_entrance_one;

fn main() {
    println!("Running puzzle");
    let data = data_provider::get_data_from_path("day1.txt");
    secret_entrance_one::run_part_one(&data);
    println!("\n");
    secret_entrance_one::run_part_two(&data);
}
