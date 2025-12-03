#[cfg(test)]
use crate::data_provider;

pub fn run_part_one(string_data: &Vec<String>) -> i32 {
    let mut current_index = 50;
    let mut result = 0;
    for line in string_data {
        if line.is_empty() {
            continue;
        }

        let dir = line.as_bytes()[0] as char;
        let num: i32 = line[1..].parse().unwrap();
        match dir {
            'L' => current_index -= num,
            'R' => current_index += num,
            _ => continue, // ignore unexpected lines
        }

        current_index = ((current_index % 100) + 100) % 100;

        if current_index == 0 {
            result += 1;
        }
    }

    println!("\nPart One Result {}", result);
    result
}

pub fn run_part_two(string_data: &Vec<String>) -> i32 {
    let mut current_index: i32 = 50;
    let mut result: i32 = 0;

    for line in string_data {
        if line.is_empty() {
            continue;
        }

        let dir = line.as_bytes()[0] as char;
        let num: i32 = line[1..].parse().unwrap();
        let old = current_index;
        let passes = match dir {
            'R' => {
                let end_linear = old + num;
                let visits = end_linear / 100;
                current_index = end_linear.rem_euclid(100);
                visits
            }
            'L' => {
                let end_linear = old - num;
                let visits = if old == 0 {
                    num / 100
                } else if num >= old {
                    (num - old) / 100 + 1
                } else {
                    0
                };
                current_index = end_linear.rem_euclid(100);
                visits
            }
            _ => continue,
        };

        result += passes;
    }

    println!("\nPart Two Result {}", result);
    result
}

#[test]
#[ignore]
fn check_result() {
    let values = "L68\nL30\nR48\nL5\nR60\nL55\nL1\nL99\nR14\nL82";
    let result = vec![
        "L68", "L30", "R48", "L5", "R60", "L55", "L1", "L99", "R14", "L82",
    ];

    let data = data_provider::get_data_from_string(values);

    let expected_data: Vec<String> = result.iter().map(|s| s.to_string()).collect();

    assert_eq!(data, expected_data);

    assert_eq!(run_part_one(&data), 3);
    assert_eq!(run_part_two(&data), 6);
}
