use crate::data_provider;

pub fn run_part_one(data: &str) -> i64 {
    let grid: Vec<Vec<char>> = data_provider::get_data_when_lines(data)
        .into_iter()
        .map(|line| line.chars().collect())
        .collect();

    let mut sum = 0;

    for (i, row) in grid.iter().enumerate() {
        for (j, &c) in row.iter().enumerate() {
            if c == '@' && count_at_signs_in_neighbors(&grid, i, j) < 5 {
                sum += 1;
            }
        }
    }

    println!("Result for printing department part one is {sum}");
    sum
}

pub fn run_part_two(data: &str) -> i64 {
    let mut grid: Vec<Vec<char>> = data_provider::get_data_when_lines(data)
        .into_iter()
        .map(|line| line.chars().collect())
        .collect();

    let mut total_removed = 0;

    loop {
        let mut to_remove = Vec::new();

        for i in 0..grid.len() {
            for j in 0..grid[i].len() {
                if grid[i][j] == '@' && count_at_signs_in_neighbors(&grid, i, j) < 5 {
                    to_remove.push((i, j));
                }
            }
        }

        if to_remove.is_empty() {
            break;
        }

        for (i, j) in to_remove {
            grid[i][j] = '.';
            total_removed += 1;
        }
    }

    println!("Result for printing department part two is {total_removed}");
    total_removed
}

fn count_at_signs_in_neighbors(grid: &[Vec<char>], row: usize, col: usize) -> usize {
    let mut count = 0;
    let row_start = row.saturating_sub(1);
    let row_end = (row + 2).min(grid.len());

    for r in row_start..row_end {
        let col_start = col.saturating_sub(1);
        let col_end = (col + 2).min(grid[r].len());

        for c in col_start..col_end {
            if grid[r][c] == '@' {
                count += 1;
            }
        }
    }

    count
}

#[ignore]
#[test]
fn printing_department_test() {
    let values = "..@@.@@@@.\n@@@.@.@.@@\n@@@@@.@.@@\n@.@@@@..@.\n@@.@@@@.@@\n.@@@@@@@.@\n.@.@.@.@@@\n@.@@@.@@@@\n.@@@@@@@@.\n@.@.@@@.@.";
    let values = values.trim();
    let data = data_provider::get_data_when_lines(values);

    assert_eq!("@.@@@@..@.", data[3]);
    assert_eq!(13, run_part_one(values));
    assert_eq!(43, run_part_two(values));
    //assert_eq!(3121910778619, crate::lobby::run_part_two(values));
}
