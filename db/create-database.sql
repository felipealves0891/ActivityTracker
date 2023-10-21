create schema activities;

create sequence activity_seq
    start 1
    increment 1
    NO MAXVALUE
    CACHE 1;
	
create table activities.activity (
	id 					bigint not null primary key,
	uuid				varchar(120) not null unique,
	machine_name		varchar(1024) not null,
	process_name		varchar(1024) not null,
	window_title		varchar(1024) not null,
	start_time			timestamp  not null,
	end_time			timestamp  not null
);

ALTER TABLE activities.activity ALTER COLUMN id SET DEFAULT nextval('activity_seq'::regclass);