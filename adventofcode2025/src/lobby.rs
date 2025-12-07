use crate::data_provider;

pub fn run_part_one(data: &str) -> i64 {
    let data_vector = data_provider::get_data_when_lines(data);
    0
}

pub fn run_part_two(data: &str) -> i64 {
    let data_vector = data_provider::get_data_when_lines(data);
    0
}

#[test]
fn lobby_test() {
    let values = "987654321111111\n811111111111119\n818181911112111";
    let values= values.trim();
    let data = data_provider::get_data_when_lines(values);
    
    assert_eq!("811111111111119", data[1]);
    //assert_eq!(357, run_part_one(values));
}
