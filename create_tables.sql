CREATE TABLE `TB_ROOM` (
     `id` INT NOT NULL AUTO_INCREMENT,
     `name` LONGTEXT NOT NULL,
     `beds` INT,
	 `status` int,
	PRIMARY KEY (`id`)
);

CREATE TABLE `TB_RESERVATION` (
     `id` INT NOT NULL AUTO_INCREMENT,
     `guest_name` LONGTEXT NOT NULL,
     `guest_email` LONGTEXT NULL,
     `guest_phone` LONGTEXT NULL,
	 `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	 `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	 `status` int,
	PRIMARY KEY (`id`)
);

CREATE TABLE `TB_BOOKINGS` (
     `id` INT NOT NULL AUTO_INCREMENT,
	 `room_id` INT NOT NULL,
	 `reservation_id` INT NOT NULL,
     `booked_from` timestamp NULL,
     `booked_to` timestamp NULL,
     `created_at` timestamp NULL,
     `updated_at` timestamp NULL,
	 `status` int,
	PRIMARY KEY (`id`),
	KEY `fk_reservation` (`reservation_id`),
	KEY `fk_reservationroom` (`room_id`),
	CONSTRAINT `fk_reservation` FOREIGN KEY (`reservation_id`) REFERENCES `TB_RESERVATION` (`id`),
	CONSTRAINT `fk_reservationroom` FOREIGN KEY (`room_id`) REFERENCES `TB_ROOM` (`id`)
);