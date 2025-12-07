use crate::data_provider;

pub fn run_part_one(data: &str) -> i64 {
    let data_vector = data_provider::get_data_when_comma_separated(data);
    let mut sum = 0;

    for range in &data_vector {
        let (start, end) = if let Some((a, b)) = range.split_once('-') {
            let start: i64 = a.trim().parse().unwrap();
            let end: i64 = b.trim().parse().unwrap();
            (start, end)
        } else {
            let id: i64 = range.trim().parse().unwrap();
            if check_duplicate(id) {
                sum += id;
            }
            continue;
        };

        for id in start..=end {
            if check_duplicate(id) {
                sum += id;
            }
        }
    }

    println!("Result for gift shop part one is {sum}");
    sum
}

pub fn run_part_two(data: &str) -> i64 {
    let data_vector = data_provider::get_data_when_comma_separated(data);
    let mut sum = 0;

    for range in &data_vector {
        let (start, end) = if let Some((a, b)) = range.split_once('-') {
            let start: i64 = a.trim().parse().unwrap();
            let end: i64 = b.trim().parse().unwrap();
            (start, end)
        } else {
            let id: i64 = range.trim().parse().unwrap();
            if check_multicate(id) {
                sum += id;
            }
            continue;
        };

        for id in start..=end {
            if check_multicate(id) {
                sum += id;
            }
        }
    }

    println!("Result for gift shop part two is {sum}");
    sum
}

fn check_duplicate(id: i64) -> bool {
    let string_id = id.to_string();
    if is_length_odd(&string_id) {
        return false;
    }

    let mid = &string_id.len() / 2;
    let (left, right) = string_id.split_at(mid);
    let parts = (&string_id[..mid], &string_id[mid..]);
    left == right
}

fn is_length_odd(s: &str) -> bool {
    s.len() % 2 == 1
}

fn check_multicate(id: i64) -> bool {
    let s = id.to_string();
    let bytes = s.as_bytes();
    let len = bytes.len();

    if len <= 1 {
        return false;
    }

    if bytes.iter().all(|&b| b == bytes[0]) {
        return true;
    }

    for pat_len in 1..len {
        if len % pat_len != 0 {
            continue;
        }

        if len / pat_len <= 1 {
            continue;
        }

        let pat = &bytes[..pat_len];
        if bytes.chunks(pat_len).all(|chunk| chunk == pat) {
            return true;
        }
    }

    false
}

#[ignore]
#[test]
fn gift_shop_test() {
    let values = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,\
    1698522-1698528,446443-446449,38593856-38593862,\
    565653-565659,824824821-824824827,2121212118-2121212124";

    let data = data_provider::get_data_when_comma_separated(values);

    assert_eq!(data[5], "1698522-1698528");

    assert_eq!(1227775554, run_part_one(values));
    assert_eq!(4174379265, run_part_two(values))
}
