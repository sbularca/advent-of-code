use crate::data_provider;

pub fn run_part_one(data: &str) -> i64 {
    let elements: Vec<String> = data_provider::get_data_when_lines(data);
    let range: Vec<(i64, i64)> = elements
        .iter()
        .filter(|s| !s.is_empty())
        .filter(|s| s.contains('-'))
        .map(|s| s.split('-').collect())
        .map(|parts: Vec<&str>| {
            let start: i64 = parts[0].trim().parse().unwrap();
            let end: i64 = parts[1].trim().parse().unwrap();
            (start, end)
        })
        .collect();
    for (start, end) in range {
        println!("{start}-{end}");
    }
    let sum = 0;
    println!("Result for cafeteria part one is {sum}");
    sum
}

pub fn run_part_two(data: &str) -> i64 {
    let sum = 0;
    println!("Result for cafeteria part two is {sum}");
    sum
}

#[test]
fn gift_shop_test() {
    let values = "3-5\n10-14\n16-20\n12-18";
    let data = data_provider::get_data_when_lines(values);
    assert_eq!(data[2], "16-20");
    // assert_eq!(1227775554, crate::gift_shop::run_part_one(values));
    // assert_eq!(4174379265, crate::gift_shop::run_part_two(values))
}
