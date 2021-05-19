﻿import Calendar, { ISchedule } from 'tui-calendar';
import "tui-calendar/dist/tui-calendar.css";
import "bootstrap/dist/css/bootstrap.min.css"

// If you use the default popups, use this.
import 'tui-date-picker/dist/tui-date-picker.css';
import 'tui-time-picker/dist/tui-time-picker.css';
import { Availability, Schedule } from './Model/Schedule';

async function main() {
    let scheduleId = 0;

    var cal = new Calendar('#calendar', {
        useCreationPopup: true,
        useDetailPopup: true,
        defaultView: 'week', // set 'week' or 'day'
        taskView: true,  // e.g. true, false, or ['task', 'milestone'])
        scheduleView: ['time']  // e.g. true, false, or ['allday', 'time'])
    });

    let endDate = cal.getDateRangeEnd().toDate();
    endDate.setDate(endDate.getDate() + 1);
    let startDate = cal.getDateRangeStart().toDate();

    var getSchedulesUrl = new URL(location.origin + '/home/getSchedules');
    getSchedulesUrl.searchParams.append('start', startDate.toJSON());
    getSchedulesUrl.searchParams.append('end', endDate.toJSON());

    let result = await fetch(getSchedulesUrl.toString(), {
        method: 'GET',
        headers: new Headers({ 'content-type': 'application/json' })});

    if (!result.ok) {
        alert('Failed to get the calendar. Try refreshing.')
        return;
    }

    let schedules = await result.json() as Schedule[];
    cal.createSchedules(schedules.map<ISchedule>(schedule => ({
        id: String(scheduleId++),
        title: schedule.title,
        category: 'time',
        state: schedule.availability == Availability.Busy ? 'Busy' : 'Free',
        start: schedule.start,
        end: schedule.end,
    })));

    cal.on('beforeCreateSchedule', async function (event: ISchedule) {
        event.category = 'time';
        cal.createSchedules([event]);

        if (!event.title) {
            alert("No title");
            return;
        }

        if (!event.location) {
            event.location = "";
        }

        if (!event.state) {
            alert("No state");
            return;
        }

        if (!event.start) {
            alert("No start");
            return;
        }

        if (!event.end) {
            alert("No end");
            return;
        }

        let poco = new Schedule(
            event.title,
            event.location,
            event.state == 'Busy' ? Availability.Busy : Availability.Free,
            new Date(event.start.valueOf() as number),
            new Date(event.end.valueOf() as number));

        let result = await fetch('home/addschedule', {
            method: 'POST',
            headers: new Headers({ 'content-type': 'application/json' }),
            body: JSON.stringify(poco),
        })


        if (!result.ok) {
            alert('Adding probably failed. Try refreshing the page.')
        }
    });
}

main();