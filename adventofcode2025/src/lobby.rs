use crate::data_provider;
use std::cmp::Reverse;
use std::iter::Cycle;
use std::num::NonZeroUsize;
use std::ops::{Add, Deref};
use std::process::id;

pub fn run_part_one(data: &str) -> i64 {
    let data_vector = data_provider::get_data_when_lines(data);
    let mut sum = 0;
    for data in data_vector {
        let set = find_closest_to_9(&data);
        if let Some((idx, c)) = set {
            println!("index = {idx}, char = {c}");
            let mut left_digit = -1;
            let mut right_digit = -1;

            if idx != 0 {
                let left_set: String = data[0..idx].to_string();
                let left_char = find_closest_to_9(&left_set);
                if let Some((idx, c1)) = left_char {
                    left_digit = format!("{c1}{c}").parse().unwrap();
                }
            }

            if idx < data.len() - 1 {
                let right_set: String = data[(idx + 1)..].to_string();
                let right_char = find_closest_to_9(&right_set);
                if let Some((idx, c2)) = right_char {
                    right_digit = format!("{c}{c2}").parse().unwrap();
                }
            }

            if left_digit > right_digit {
                sum += left_digit;
                continue;
            }
            sum += right_digit;
        }
    }
    println!("Result for lobby part one is {sum}");
    sum
}

fn find_closest_to_9(data: &String) -> Option<(usize, char)> {
    let set = data
        .chars()
        .enumerate()
        .min_by_key(|(idx, c)| {
            let d = c.to_string().parse::<i32>().unwrap();
            ((9 - d).abs(), *idx as i32)
        })
        .map(|(idx, c)| (idx, c));
    set
}

pub fn run_part_two(data: &str) -> i64 {
    let data_vector = data_provider::get_data_when_lines(data);
    let mut sum = 0;

    for mut data in data_vector {
        let mut group: Vec<(usize, char)> = Vec::with_capacity(12);
        let substring = data[0..data.len() - 12].to_string();
        let mut start_index = 0;
        let current = find_closest_to_9(&substring);

        if let Some((idx, c)) = current {
            group.push((idx, c));
            start_index = idx;
        }

        sum = recursive_check(data, &mut group);

        // for cycle in 0..12{
        //     let mut t_data = &data;
        //     let current = find_closest_to_9(&t_data);
        //     if let Some((idx, c)) = current{
        //         if (&data.len()-idx == 12){
        //             group.push((idx, c));
        //             let result: i64 = data[idx..data.len()-1].parse().unwrap();
        //             sum += result;
        //             break
        //         }
        //     }
        // }
        // group.sort_by_key(|(index, _ch)| *index);
        // let s: String = group
        //     .iter()
        //     .map(|&(_idx, c)| c)   // take only the char from each (usize, char)
        //     .collect();
        // let number: i64 = s.parse().unwrap();
        // sum += number;
    }

    println!("\nResult for lobby part two is {sum}");
    sum
}

fn recursive_check(substring: String, group: &mut Vec<(usize, char)>) -> i64 {
    let mut sum: i64 = 0;
    let mut i = 0;
    let mut index = 0;
    let mut word = String::new();
    while i < 12 {
        i += 1;
        if (index < substring.len() - 12) {
            let mut substring = substring[index..substring.len() - 1].to_string();
            let current = find_closest_to_9(&substring);

            if let Some((idx, c)) = current {
                group.push((idx, c));
                index = idx;
            }
            continue;
        }
        for c in substring.chars() {
            group.push((0, c));
        }
        break;
    }
    for set in group {
        word.push(set.1);
        sum += word.parse::<i64>().unwrap();
    }
    print!("\n{word}");
    sum
}

#[ignore]
#[test]
fn lobby_test() {
    let values = "987654321111111\n811111111111119\n234234234234278\n818181911112111";
    let values = values.trim();
    let data = data_provider::get_data_when_lines(values);

    assert_eq!("811111111111119", data[1]);
    assert_eq!(357, run_part_one(values));
    assert_eq!(3121910778619, run_part_two(values));
}
