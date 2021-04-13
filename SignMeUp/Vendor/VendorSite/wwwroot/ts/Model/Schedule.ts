export enum Availability { Busy, Free };

export class Schedule {
    constructor(
        public title: string,
        public location: string,
        public availability: Availability,
    ) { }
}