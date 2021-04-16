import { DateType } from "tui-calendar";

export enum Availability { Busy, Free };

export class Schedule {
    constructor(
        public title: string,
        public location: string,
        public availability: Availability,
        public start: Date,
        public end: Date,
    ) {

    }
}